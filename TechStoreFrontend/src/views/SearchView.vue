<script setup>
import { computed, ref, watch } from "vue";
import { useRoute } from "vue-router";
import ImageSlider from "@/components/ImageSlider.vue";
import ProductList from "@/components/ProductList.vue";
import { getProductsByTitle } from "@/database/services/productService";

const route = useRoute();
const products = ref(getProductsByTitle(route.query.q));
const sortType = ref("");

watch(
  () => route.query.q,
  (newQueryTerm) => {
    products.value = getProductsByTitle(newQueryTerm);
  }
);

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
</script>

<template>
  <div>
    <image-slider maxHeight="400px" />
    <div class="main w-75">
      <div class="heading">
        <h1 class="heading__title">Search results: {{ route.query.q }}</h1>
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
      <product-list :products="sortedProducts" />
    </div>
  </div>
</template>

<style scoped>
.main {
  margin: 5rem auto;
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
</style>
