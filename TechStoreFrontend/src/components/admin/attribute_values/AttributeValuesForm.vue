<script setup>
import { computed, reactive, ref, watch } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import BaseInput from "@/components/common/BaseInput.vue";
import FormContainer from "@/components/common/FormContainer.vue";
import { initialAttributeValueState, attributeValueRules } from "@/vuelidate/attributeValue";
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
const attributeValueState = reactive({ ...initialAttributeValueState });
const loading = ref(false);

const v$ = useVuelidate(attributeValueRules, attributeValueState);

watch(
  () => props.id,
  (newId) => {
    if (newId) {
      loadAttributeValue(newId);
    } else {
      resetForm();
    }
  },
  { immediate: true }
);

const formTitle = computed(() => (props.id ? "Edit Attribute Value" : "New Attribute Value"));

async function loadAttributeValue(id) {
  try {
    const { data, status } = await axiosPrivate.get(`/attribute-values/${id}`);

    if (status === 200) {
      attributeValueState.value = data?.value;
    }
  } catch (error) {
    toast.error("Failed to load attribute value.");
    console.error(error);
  }
}

const handleSave = async () => {
  if (!(await v$.value.$validate())) return;

  loading.value = true;

  const payload = {
    value: attributeValueState?.value,
  };

  try {
    let resp;

    if (props?.id) {
      resp = await axiosPrivate.put(`/attribute-values/${props.id}`, payload);
    } else {
      resp = await axiosPrivate.post("/attribute-values", payload);
    }

    if (resp.status == 200) {
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
  Object.assign(attributeValueState, initialAttributeValueState);
}
</script>

<template>
  <form-container :title="formTitle">
    <v-form @submit.prevent="handleSave">
      <base-input
        v-model="attributeValueState.value"
        class="mt-5"
        name="value"
        label="Value"
        :v$="v$.value"
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
