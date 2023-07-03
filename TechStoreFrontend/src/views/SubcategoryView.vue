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
const breadcrumbsItems = [
  {
    text: "Home",
    disabled: false,
    href: "/",
  },
  {
    text: `${categorySlug.value}`,
    disabled: false,
    href: `/${categorySlug.value}`,
  },
  {
    text: `${subcategorySlug.value}`,
    disabled: true,
    href: `/${categorySlug.value}/${subcategorySlug.value}`,
  },
];

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

function handleFilterPrice(price, filters) {
  handleFilter(filters);

  if (!price.from && !price.to) return;

  products.value = products.value.filter(
    (product) => product.price > price.from && product.price < price.to
  );
}

function handleFilterRating(rating, filters) {
  handleFilter(filters);
  products.value = products.value.filter((product) => product.rating > rating);
}

function handleFilter(filters) {
  products.value = getFilteredProductsBySubcategoryId(subcategory.id, filters);
}
</script>

<template>
  <div>
    <image-slider maxHeight="400px" />
    <v-breadcrumbs class="text-capitalize ml-16" :items="breadcrumbsItems">
      <template v-slot:divider>
        <v-icon>mdi-chevron-right</v-icon>
      </template>
    </v-breadcrumbs>
    <div class="sidebar-layout">
      <filter-sidebar
        class="sidebar"
        :subcategoryId="subcategory.id"
        @filterPrice="handleFilterPrice"
        @filterRating="handleFilterRating"
        @filter="handleFilter"
      />
      <div class="main">
        <div class="heading">
          <h1 class="heading__title">{{ subcategory.name }}</h1>
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
  align-items: start;
  gap: 50px;
  max-width: 70%;
  margin: 2rem auto;
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
