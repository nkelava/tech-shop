<script setup>
import { ref } from "vue";
import FormContainer from "@/components/common/FormContainer.vue";

const code = ref("");
const discount = ref(0); // TODO: check => 0 && <= 100
const loading = ref(false);

function handleSave() {
  const formData = new FormData();
  formData.append("code", code.value);
  formData.append("discount", discount.value);

  console.log(formData);
}
</script>

<template>
  <form-container title="New Promo Code">
    <v-form @submit.prevent>
      <v-text-field
        v-model="code"
        class="mt-5"
        label="Code"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
      <v-text-field
        v-model="discount"
        type="number"
        class="mt-5"
        label="Discount"
        density="compact"
        variant="outlined"
        hide-details="auto"
        min="0"
        max="100"
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
