<script setup>
import { ref } from "vue";
import FormContainer from "@/components/common/FormContainer.vue";

const name = ref("");
const slug = ref("");
const loading = ref(false);

function handleSave() {
  const formData = new FormData();
  formData.append("name", name.value);
  formData.append("slug", slug.value);

  console.log(formData);
}
</script>

<template>
  <form-container title="New Category">
    <v-form @submit.prevent>
      <v-text-field
        v-model="name"
        class="mt-5"
        label="Name"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
      <v-text-field
        v-model="slug"
        class="mt-5"
        label="Slug"
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
