<script setup>
import { useRoute } from "vue-router";
import { ref, watch } from "vue";
import TheSlider from "@/components/TheSlider.vue";
import BaseGrid from "@/components/BaseGrid.vue";
import SubcategoryCard from "@/components/SubcategoryCard.vue";
import { categories } from "@/data/categories.js";

const route = useRoute();
const category = ref(route.params.category);
const subcategories = ref(categories[category.value]);

watch(
  () => route.params.category,
  (newCategory) => {
    category.value = newCategory;
    subcategories.value = categories[newCategory];
  }
);
</script>

<template>
  <div>
    <TheSlider height="200px" />
    <div class="category-container">
      <h1 class="category__title">{{ category }}</h1>
      <hr />
      <BaseGrid>
        <SubcategoryCard
          v-for="subcategory in subcategories"
          :key="subcategory.id"
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
