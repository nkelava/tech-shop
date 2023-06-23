<script setup>
import { onMounted, ref } from "vue";
import ImageSlider from "@/components/ImageSlider.vue";
import TheBenefits from "@/components/TheBenefits.vue";
import TheBanners from "@/components/TheBanners.vue";
import ProductGrid from "@/components/ProductGrid.vue";
import ProductSlider from "@/components/ProductSlider.vue";
import { getProducts } from "@/database/services/productService";

const bestSellers = ref([]);
const newArrivals = ref([]);
const hotOffers = ref([]);
const topRated = ref([]);

onMounted(() => {
  bestSellers.value = getProducts();
  newArrivals.value = getProducts();
  hotOffers.value = getProducts();
  topRated.value = getProducts();
});
</script>

<template>
  <div>
    <image-slider maxHeight="800px" />
    <the-benefits />
    <div class="grid-container">
      <h1>Best Sellers</h1>
      <hr />
      <product-grid :products="bestSellers" />
    </div>
    <the-banners />
    <div class="carousel-container">
      <h1>New Arrivals</h1>
      <hr />
      <product-slider :products="bestSellers" />
    </div>
  </div>
</template>

<style scoped>
.grid-container,
.carousel-container {
  max-width: 70%;
  margin: 6rem auto;
}
</style>
