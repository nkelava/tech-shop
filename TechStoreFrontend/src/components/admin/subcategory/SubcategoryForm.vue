<script setup>
import { computed, onMounted, reactive, ref, watch } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import BaseInput from "@/components/common/BaseInput.vue";
import FormContainer from "@/components/common/FormContainer.vue";
import { initialSubcategoryState, subcategoryRules } from "@/vuelidate/subcategory";
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
const categories = ref([]);
const subcategoryState = reactive({ ...initialSubcategoryState });
const loading = ref(false);

const v$ = useVuelidate(subcategoryRules, subcategoryState);

onMounted(async () => {
  await axiosPrivate
    .get("/categories")
    .then((resp) => {
      if (resp.status !== 200) return;
      categories.value = resp.data;
    })
    .catch((error) => {
      toast.error("Failed to load categories.");
      console.log(error);
    });
});

watch(
  () => props?.id,
  (newId) => {
    if (newId) {
      loadSubcategory(newId);
    } else {
      resetForm();
    }
  },
  { immediate: true }
);

const formTitle = computed(() => (props.id ? "Edit Subcategory" : "New Subcategory"));

async function loadSubcategory(id) {
  try {
    const { data, status } = await axiosPrivate.get(`/subcategories/${id}`);

    if (status === 200) {
      subcategoryState.name = data?.name;
      subcategoryState.slug = data?.slug;
      subcategoryState.image = data?.imageURL;
      subcategoryState.category = data?.category?.id;
    }
  } catch (error) {
    toast.error("Failed to load subcategory.");
    console.error(error);
  }
}

const handleSave = async () => {
  if (!(await v$.value.$validate())) return;

  loading.value = true;

  const payload = {
    name: subcategoryState?.name,
    slug: subcategoryState?.slug,
    imageURL: subcategoryState?.image,
    categoryId: subcategoryState?.category,
  };

  try {
    let resp;

    if (props?.id) {
      resp = await axiosPrivate.put(`/subcategories/${props.id}`, payload);
    } else {
      resp = await axiosPrivate.post("/subcategories", payload);
    }

    if (resp.status == 200) {
      resetForm();
      toast.success(props?.id ? ITEM_UPDATE_SUCCESS : ITEM_CREATE_SUCCESS);
      emit("clearSelectedId");
      emit("reload");
    }
  } catch (error) {
    toast.error(
      error?.response?.data ? error.response.data : props?.id ? ITEM_UPDATE_FAIL : ITEM_CREATE_FAIL
    );
    console.log(error);
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
  Object.assign(subcategoryState, initialSubcategoryState);
}
</script>

<template>
  <form-container :title="formTitle">
    <v-form @submit.prevent="handleSave">
      <base-input
        v-model="subcategoryState.name"
        class="mt-5"
        name="name"
        label="Name"
        :v$="v$.name"
        density="compact"
        hide-details="auto"
      />
      <base-input
        v-model="subcategoryState.slug"
        class="mt-5"
        name="slug"
        label="Slug"
        :v$="v$.slug"
        density="compact"
        hide-details="auto"
      />
      <!-- TODO: Add image upload -->
      <base-input
        v-model="subcategoryState.image"
        class="mt-5"
        name="image"
        label="Image URL"
        density="compact"
        hide-details="auto"
      />
      <v-select
        v-model="subcategoryState.category"
        class="mt-5 test"
        name="category"
        label="Category"
        :items="categories"
        item-value="id"
        item-title="name"
        density="compact"
        hide-details="auto"
        variant="outlined"
        :error-messages="v$?.category?.$errors.map((e) => e.$message)"
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

.test * {
  color: coral;
}
</style>
