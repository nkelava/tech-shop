<script setup>
import { ref, watch } from "vue";
import { useRoute } from "vue-router";
import ImageSlider from "@/components/ImageSlider.vue";
import BaseGrid from "@/components/BaseGrid.vue";
import SubcategoryCard from "@/components/SubcategoryCard.vue";
import { getCategoryBySlug } from "@/database/services/categoryService.js";
import { getSubcategoriesByCategoryId } from "@/database/services/subcategoryService.js";

const route = useRoute();
const categorySlug = ref(route.params.category);
let category = getCategoryBySlug(categorySlug.value);
const subcategories = ref(getSubcategoriesByCategoryId(category.id));
const breadcrumbsItems = [
  {
    text: "Home",
    disabled: false,
    href: "/",
  },
  {
    text: `${categorySlug.value}`,
    disabled: true,
    href: `/${categorySlug.value}`,
  },
];

watch(
  () => route.params.category,
  (newCategory) => {
    categorySlug.value = newCategory;
    category = getCategoryBySlug(categorySlug.value);
    subcategories.value = getSubcategoriesByCategoryId(category.id);
  }
);
</script>

<template>
  <div>
    <image-slider maxHeight="400px" />
    <v-breadcrumbs class="text-capitalize ml-16" :items="breadcrumbsItems">
      <template v-slot:divider>
        <v-icon>mdi-chevron-right</v-icon>
      </template>
    </v-breadcrumbs>
    <div class="category-container">
      <h1 class="category__title text-capitalize">{{ category.name }}</h1>
      <hr />
      <base-grid>
        <subcategory-card
          v-for="subcategory in subcategories"
          :key="subcategory.id"
          :category="category.name"
          :subcategory="subcategory"
        />
      </base-grid>
    </div>
  </div>
</template>

<style scoped>
.category-container {
  padding: 2rem 5rem;
}

.category__list {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  justify-content: center;
  gap: 1rem;
}
</style>
