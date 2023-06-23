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
const tab = ref("new");

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
    <div class="tabs-container">
      <div class="tabs__header">
        <v-tabs v-model="tab">
          <v-tab value="new">
            <h2 class="tab">New Arrivals</h2>
          </v-tab>
          <v-tab value="hot">
            <h2 class="tab">Hot Offers</h2>
          </v-tab>
          <v-tab value="top">
            <h2 class="tab">Top Rated</h2>
          </v-tab>
        </v-tabs>
      </div>
      <v-card-text>
        <v-window v-model="tab" class="tabs__content">
          <v-window-item value="new">
            <product-slider :products="newArrivals" />
          </v-window-item>

          <v-window-item value="hot">
            <product-slider :products="hotOffers" />
          </v-window-item>

          <v-window-item value="top">
            <product-slider value="three" :products="topRated" />
          </v-window-item>
        </v-window>
      </v-card-text>
    </div>
  </div>
</template>

<style scoped>
.grid-container,
.tabs-container {
  max-width: 70%;
  margin: 6rem auto;
}

.tabs__header {
  margin-bottom: 3rem;
}

.tab {
  text-transform: capitalize;
}

.tabs__content {
  overflow: visible;
}
</style>
