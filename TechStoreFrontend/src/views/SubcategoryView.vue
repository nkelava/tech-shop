<script setup>
import { ref, computed, onMounted } from "vue";
import { useRoute } from "vue-router";
import ImageSlider from "@/components/ImageSlider.vue";
import ProductGrid from "@/components/ProductGrid.vue";
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
    <image-slider maxHeight="400px" />
    <div class="sidebar-layout">
      <filter-sidebar class="sidebar" :subcategoryId="subcategory.id" @filter="handleFilter" />
      <div class="main">
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
        <!-- TODO: paginated list, this is temp -->
        <product-grid :products="sortedProducts" />
      </div>
    </div>
  </div>
</template>

<style scoped>
.sidebar-layout {
  display: grid;
  grid-template-areas: "sidebar main";
  grid-template-columns: minmax(300px, 350px) auto;
  gap: 50px;
  max-width: 70%;
  margin: 6rem auto;
}

.sidebar {
  grid-area: sidebar;
}

.main {
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

@media only screen and (max-width: 1200px) {
  .sidebar-layout {
    grid-template-areas:
      "sidebar"
      "main";
    grid-template-columns: auto;
    gap: 1rem;
    max-width: 100%;
    margin-inline: 1rem;
  }

  .sidebar {
    min-width: 100%;
  }

  .main {
    min-width: 100%;
  }
}
</style>
