<script setup>
import { computed, reactive, ref, watch } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import BaseInput from "@/components/common/BaseInput.vue";
import FormContainer from "@/components/common/FormContainer.vue";
import { initialCategoryState, categoryRules } from "@/vuelidate/category";
import { ITEM_CREATE_FAIL, ITEM_CREATE_SUCCESS } from "@/constants/messages/create";
import { ITEM_UPDATE_FAIL, ITEM_UPDATE_SUCCESS } from "@/constants/messages/update";

const props = defineProps({
  id: {
    type: [String, Number],
    required: false,
    default: null,
  },
});
const emit = defineEmits(["reload", "clearSelectedId"]);
const toast = useToast();
const categoryState = reactive({ ...initialCategoryState });
const loading = ref(false);

const v$ = useVuelidate(categoryRules, categoryState);

watch(
  () => props.id,
  (newId) => {
    if (newId) {
      loadCategory(newId);
    } else {
      resetForm();
    }
  },
  { immediate: true }
);

const formTitle = computed(() => (props.id ? "Edit Category" : "New Category"));

async function loadCategory(id) {
  try {
    const { data, status } = await axiosPrivate.get(`/categories/${id}`);

    if (status === 200) {
      categoryState.name = data?.name;
      categoryState.slug = data?.slug;
    }
  } catch (error) {
    toast.error("Failed to load category.");
    console.error(error);
  }
}

const handleSave = async () => {
  if (!(await v$.value.$validate())) return;

  loading.value = true;

  const payload = {
    name: categoryState?.name,
    slug: categoryState?.slug,
  };

  try {
    let resp;

    if (props?.id) {
      resp = await axiosPrivate.put(`/categories/${props.id}`, payload);
    } else {
      resp = await axiosPrivate.post("/categories", payload);
    }

    if (resp?.status == 200) {
      resetForm();
      toast.success(props?.id ? ITEM_UPDATE_SUCCESS : ITEM_CREATE_SUCCESS);
      emit("clearSelectedId");
      emit("reload");
    }
  } catch (error) {
    console.log(error);
    toast.error(props?.id ? ITEM_UPDATE_FAIL : ITEM_CREATE_FAIL);
  } finally {
    loading.value = false;
  }
};

const handleCancel = () => {
  resetForm();
  emit("clearSelectedId");
};

function resetForm() {
  props.id = null;
  v$.value.$reset();
  Object.assign(categoryState, initialCategoryState);
}
</script>

<template>
  <form-container :title="formTitle">
    <v-form @submit.prevent="handleSave">
      <base-input
        v-model="categoryState.name"
        class="mt-5"
        name="name"
        label="Name"
        :v$="v$.name"
        density="compact"
        hide-details="auto"
      />
      <base-input
        v-model="categoryState.slug"
        class="mt-5"
        name="slug"
        label="Slug"
        :v$="v$.slug"
        density="compact"
        hide-details="auto"
      />
      <v-btn
        v-if="props?.id"
        type="button"
        class="form__btn mr-5"
        :loading="loading"
        :disabled="loading"
        @click="handleCancel"
        >Cancel</v-btn
      >
      <v-btn type="submit" class="form__btn" :loading="loading" :disabled="loading">Save</v-btn>
    </v-form>
  </form-container>
</template>

<style scoped>
.form__btn {
  margin-top: 30px;
  font-weight: bold;
  background: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
  width: 100%;
  max-width: 300px !important;
}
</style>
