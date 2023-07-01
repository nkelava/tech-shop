<script setup>
import { ref } from "vue";
import { useRoute } from "vue-router";
import ProductGallery from "@/components/ProductGallery.vue";
import ProductDetails from "../components/ProductDetails.vue";
import TabsWrapper from "@/components/TabsWrapper.vue";

// import { getProductById } from "@/database/services/productService.js";
import { getProductWithSpecification } from "../database/services/productService";

const route = useRoute();
const categorySlug = ref(route.params.category);
const subcategorySlug = ref(route.params.subcategory);
const productId = ref(route.params.productId);
const product = getProductWithSpecification(Number(productId.value));
</script>

<template>
  <div>
    <v-breadcrumbs
      class="text-capitalize ml-16"
      :items="['Home', `${categorySlug}`, `${subcategorySlug}`, `${product.name}`]"
    />
    <div class="product-container">
      <product-gallery class="gallery" />
      <product-details class="info" :product="product" />
      <tabs-wrapper class="tabs" :product="product" />
    </div>
  </div>
</template>

<style scoped>
.product-container {
  display: grid;
  grid-template-areas:
    "gallery info"
    "tabs tabs";
  grid-template-columns: 0.9fr 1fr;
  padding: 2rem;
  max-width: 100%;
}

.gallery {
  grid-area: gallery;
}

.info {
  grid-area: info;
}

.tabs {
  grid-area: tabs;
  margin-top: 5rem;
}

@media (max-width: 1000px) {
  .product-container {
    grid-template-areas:
      "gallery"
      "info"
      "tabs";

    grid-template-columns: 1fr;
  }
}

@media (min-width: 1450px) {
  .tabs {
    width: 75%;
    margin-inline: auto;
  }
}
</style>
