<script setup>
import { ref, computed, onMounted } from "vue";
import { useRoute } from "vue-router";
import TheSlider from "@/components/TheSlider.vue";
import ProductList from "@/components/ProductList.vue";
import FilterSidebar from "@/components/TheFilterSidebar.vue";
import { getSubcategoryBySlug } from "@/database/services/subcategoryService.js";
import {
  getProductsBySubcategoryId,
  getFilteredProductsBySubcategoryId,
} from "@/database/services/productService.js";

const route = useRoute();
const categorySlug = ref(route.params.category);
const subcategorySlug = ref(route.params.subcategory);
const subcategory = getSubcategoryBySlug(subcategorySlug.value);
const products = ref([]);
const sortType = ref("");

onMounted(() => {
  products.value = getProductsBySubcategoryId(subcategory.id);
});

const updateSort = (event) => {
  sortType.value = event.target.value;
};
const sortedProducts = computed(() => {
  // TODO: maybe add sort type enum
  const sortedProducts = products.value;

  switch (sortType.value) {
    case "low":
      return sortedProducts.sort((a, b) => a.price - b.price);
    case "high":
      return sortedProducts.sort((a, b) => a.price - b.price).reverse();
    case "asc":
      return sortedProducts.sort((a, b) => a.name.localeCompare(b.name));
    case "desc":
      return sortedProducts.sort((a, b) => a.name.localeCompare(b.name)).reverse();
    default:
      return sortedProducts;
  }
});

function handleFilter(filters) {
  products.value = getFilteredProductsBySubcategoryId(subcategory.id, filters);
}
</script>

<template>
  <div>
    <TheSlider height="200px" />
    <div class="layout">
      <FilterSidebar :subcategoryId="subcategory.id" @filter="handleFilter" />
      <div class="content">
        <div class="heading">
          <h1 class="heading__title">{{ categorySlug }} > {{ subcategory.name }}</h1>
          <!-- TODO: create sort select component -->
          <select class="heading__sort" name="sort" @change="updateSort">
            <option value="" hidden>Sort...</option>
            <option value="asc">Alphabetically: A-Z</option>
            <option value="desc">Alphabetically: Z-A</option>
            <option value="low">Price: Low to High</option>
            <option value="high">Price: High to Low</option>
          </select>
        </div>
        <hr />
        <ProductList :products="sortedProducts" />
      </div>
    </div>
  </div>
</template>

<style scoped>
.layout {
  display: grid;
  grid-template-areas: "sidebar main";
  grid-template-columns: 300px auto;
  align-items: start;
  gap: 3rem;
  margin: 3rem;
}

.layout:nth-child(1) {
  grid-area: sidebar;
}

.layout:nth-child(2) {
  grid-area: main;
}

.heading {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  padding-bottom: 5px;
}

.heading__title {
  text-transform: capitalize;
}

.heading__sort {
  background-color: var(--ts-c-bg-light);
  border: none;
  border-radius: 5px;
  color: var(--ts-c-text-dark);
  padding-inline: 10px;
  height: 1.5rem;
  align-self: end;
}

hr {
  margin-bottom: 1rem;
}
</style>
