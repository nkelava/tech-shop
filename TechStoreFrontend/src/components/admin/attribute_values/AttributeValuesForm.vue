<script setup>
import { ref } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import FormContainer from "@/components/common/FormContainer.vue";
import { ITEM_CREATE_FAIL, ITEM_CREATE_SUCCESS } from "../../../constants/messages/create";

const emit = defineEmits(["reload"]);
const toast = useToast();
const attributeValue = ref("");
const loading = ref(false);

async function handleSave() {
  await axiosPrivate
    .post("/attribute-values", {
      value: attributeValue.value,
    })
    .catch(() => toast.error(ITEM_CREATE_FAIL));

  attributeValue.value = "";
  toast.success(ITEM_CREATE_SUCCESS);
  emit("reload");
}
</script>

<template>
  <form-container title="New Attribute Value">
    <v-form @submit.prevent>
      <v-text-field
        v-model="attributeValue"
        class="mt-5"
        label="Value"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
      <v-btn type="submit" class="form__btn" :loading="loading" @click="handleSave">Save</v-btn>
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
