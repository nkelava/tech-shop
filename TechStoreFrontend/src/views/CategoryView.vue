<script setup>
import { useRoute } from "vue-router";
import { ref, watch } from "vue";
import TheSlider from "@/components/TheSlider.vue";
import BaseGrid from "@/components/BaseGrid.vue";
import SubcategoryCard from "@/components/SubcategoryCard.vue";
import { categoriesDb } from "@/data/categories.js";
import { subcategoriesDb } from "@/data/subcategories.js";

const route = useRoute();
const categorySlug = ref(route.params.category);
let category = categoriesDb.find((category) => category.slug === categorySlug.value);
const subcategories = ref(
  subcategoriesDb.filter((subcategory) => subcategory.categoryId === category.id)
);

watch(
  () => route.params.category,
  (newCategory) => {
    categorySlug.value = newCategory;
    category = categoriesDb.find((category) => category.slug === categorySlug.value);
    subcategories.value = subcategoriesDb.find(
      (subcategory) => subcategory.categoryId === category.id
    );
  }
);
</script>

<template>
  <div>
    <TheSlider height="200px" />
    <div class="category-container">
      <h1 class="category__title">{{ category.name }}</h1>
      <hr />
      <BaseGrid>
        <SubcategoryCard
          v-for="subcategory in subcategories"
          :key="subcategory.id"
          :category="category.name"
          :subcategory="subcategory"
        />
      </BaseGrid>
    </div>
  </div>
</template>

<style scoped>
.category-container {
  padding: 2rem 5rem;
}

.category__title {
  text-transform: capitalize;
}

.category__list {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  justify-content: center;
  gap: 1rem;
}
</style>
