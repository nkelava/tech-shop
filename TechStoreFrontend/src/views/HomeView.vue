<script setup>
import ImageSlider from "@/components/ImageSlider.vue";
import TheBenefits from "@/components/TheBenefits.vue";
import TheBanners from "@/components/TheBanners.vue";
import ProductGrid from "@/components/ProductGrid.vue";
import ProductSlider from "@/components/ProductSlider.vue";
import { getProducts } from "@/database/services/productService";

const products = getProducts();
</script>

<template>
  <div>
    <image-slider maxHeight="800px" />
    <the-benefits />
    <product-grid class="container" header="Best Sellers" :products="products" />
    <!-- <product-slider header="Best Sellers" :products="products" /> -->
    <the-banners />
    <product-slider class="container" header="New Arrivals" :products="products" />
    <product-slider class="container" header="Hot Offers" :products="products" />
  </div>
</template>

<style scoped>
.container {
  margin: 40px auto;
  padding: 0.5rem 5rem;
}
</style>
