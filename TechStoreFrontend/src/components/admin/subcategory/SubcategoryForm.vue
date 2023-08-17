<script setup>
import { onMounted, ref } from "vue";
import FormContainer from "@/components/common/FormContainer.vue";

const categories = ref([]);
const name = ref("");
const slug = ref("");
const image = ref(null);
const category = ref(null);
const loading = ref(false);

onMounted(() => {
  categories.value = [
    { id: 0, name: "Laptops" },
    { id: 1, name: "Computers" },
    { id: 2, name: "Peripherals" },
  ];
});

function handleSave() {
  const formData = new FormData();
  formData.append("name", name.value);
  formData.append("slug", slug.value);
  formData.append("image", image.value);
  formData.append("categoryId", category.value);

  console.log(formData);
}
</script>

<template>
  <form-container title="New Subcategory">
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
      <v-file-input
        v-model="image"
        class="mt-5"
        label="Image"
        density="compact"
        variant="outlined"
        hide-details="auto"
      ></v-file-input>
      <v-select
        v-model="category"
        class="mt-5 test"
        label="Category"
        :items="categories"
        item-value="id"
        item-title="name"
        density="compact"
        hide-details="auto"
        variant="outlined"
      ></v-select>
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

.test * {
  color: coral;
}
</style>
