<script setup>
import { ref } from "vue";
import { useRoute } from "vue-router";
import ProductGallery from "@/components/ProductGallery.vue";
import ProductDetails from "../components/ProductDetails.vue";
import TabsWrapper from "@/components/TabsWrapper.vue";
import TabsItem from "@/components/TabsItem.vue";

import { getProductById } from "@/database/services/productService.js";

const route = useRoute();
const productId = ref(route.params.productId);
const product = getProductById(Number(productId.value));
</script>

<template>
  <div class="product-container">
    <ProductGallery class="gallery" />
    <ProductDetails :product="product" class="info" />
    <TabsWrapper class="tabs"> </TabsWrapper>
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
  margin-top: 3rem;
}

.spec {
  margin-top: 3rem;
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
</style>
