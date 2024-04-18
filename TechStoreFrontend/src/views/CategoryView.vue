<script setup>
import { onMounted, ref, watch } from "vue";
import { useRoute } from "vue-router";
import axios from "@/api/axios";
import ImageSlider from "@/components/ImageSlider.vue";
import BaseGrid from "@/components/common/BaseGrid.vue";
import SubcategoryCard from "@/components/SubcategoryCard.vue";

const route = useRoute();
const categorySlug = ref(route.params.category);
const category = ref({});
const breadcrumbsItems = [
  {
    title: "Home",
    disabled: false,
    href: "/",
  },
  {
    title: `${categorySlug.value}`,
    disabled: true,
  },
];

onMounted(async () => {
  await axios
    .get(`/categories/${categorySlug.value}/subcategories`)
    .then((response) => (category.value = response.data))
    .catch((error) => console.log(error));
});

watch(
  () => route.params.category,
  async (newCategory) => {
    categorySlug.value = newCategory;
    await axios
      .get(`/categories/${categorySlug.value}/subcategories`)
      .then((response) => (category.value = response.data))
      .catch((error) => console.log(error));
  }
);
</script>

<template>
  <div>
    <image-slider />
    <div class="ts-breadcrumbs">
      <v-breadcrumbs :items="breadcrumbsItems">
        <template v-slot:divider>
          <v-icon>mdi-chevron-right</v-icon>
        </template>
      </v-breadcrumbs>
    </div>
    <div class="ts-container">
      <h1 class="category__title text-capitalize">{{ category.name || categorySlug }}</h1>
      <hr />
      <base-grid v-if="category.subcategories">
        <subcategory-card
          v-for="subcategory in category.subcategories"
          :key="subcategory.categoryId"
          :category="category"
          :subcategory="subcategory"
        />
      </base-grid>
      <h3 v-else>No subcategories.</h3>
    </div>
  </div>
</template>

<style scoped>
.category__list {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  justify-content: center;
  gap: 1rem;
}
</style>
