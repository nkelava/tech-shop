<script setup>
import { computed, reactive, ref, watch } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import BaseInput from "@/components/common/BaseInput.vue";
import FormContainer from "@/components/common/FormContainer.vue";
import { initialAttributeState, attributeRules } from "@/vuelidate/attribute";
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
const attributeState = reactive({ ...initialAttributeState });
const loading = ref(false);

const v$ = useVuelidate(attributeRules, attributeState);

watch(
  () => props.id,
  (newId) => {
    if (newId) {
      loadAttribute(newId);
    } else {
      resetForm();
    }
  },
  { immediate: true }
);

const formTitle = computed(() => (props.id ? "Edit Attribute" : "New Attribute"));

async function loadAttribute(id) {
  try {
    const { data, status } = await axiosPrivate.get(`/attributes/${id}`);

    if (status === 200) {
      attributeState.name = data?.name;
    }
  } catch (error) {
    toast.error("Failed to load attribute.");
    console.error(error);
  }
}

const handleSave = async () => {
  if (!(await v$.value.$validate())) return;

  loading.value = true;

  const payload = {
    name: attributeState?.name,
  };

  try {
    let resp;

    if (props?.id) {
      resp = await axiosPrivate.put(`/attributes/${props.id}`, payload);
    } else {
      resp = await axiosPrivate.post("/attributes", payload);
    }

    if (resp?.status == 200) {
      resetForm();
      toast.success(props?.id ? ITEM_UPDATE_SUCCESS : ITEM_CREATE_SUCCESS);
      emit("clearSelectedId");
      emit("reload");
    }
  } catch (error) {
    toast.error(props?.id ? ITEM_UPDATE_FAIL : ITEM_CREATE_FAIL);
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
  Object.assign(attributeState, initialAttributeState);
}
</script>

<template>
  <form-container :title="formTitle">
    <v-form @submit.prevent="handleSave">
      <base-input
        v-model="attributeState.name"
        class="mt-5"
        name="name"
        label="Name"
        :v$="v$.name"
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
