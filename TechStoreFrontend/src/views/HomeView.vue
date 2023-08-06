<script setup>
import { onMounted, ref } from "vue";
import ImageSlider from "@/components/ImageSlider.vue";
import TheBenefits from "@/components/TheBenefits.vue";
import TheBanners from "@/components/TheAds.vue";
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
    <div class="best-sellers ts-container">
      <h1>Best Sellers</h1>
      <hr />
      <product-grid :products="bestSellers" />
    </div>
    <the-banners />
    <div class="w-75 my-16 mx-auto">
      <div class="mb-15">
        <!-- TODO: fix tabs on mobile -->
        <v-tabs v-model="tab">
          <v-tab value="new">
            <h2 class="text-capitalize">New Arrivals</h2>
          </v-tab>
          <v-tab value="hot">
            <h2 class="text-capitalize">Hot Offers</h2>
          </v-tab>
          <v-tab value="top">
            <h2 class="text-capitalize">Top Rated</h2>
          </v-tab>
        </v-tabs>
      </div>
      <v-card-text>
        <v-window v-model="tab" class="overflow-visible">
          <v-window-item value="new">
            <product-slider :products="newArrivals" />
          </v-window-item>

          <v-window-item value="hot">
            <product-slider :products="hotOffers" />
          </v-window-item>

          <v-window-item value="top">
            <product-slider :products="topRated" />
          </v-window-item>
        </v-window>
      </v-card-text>
    </div>
  </div>
</template>

<style scoped>
.best-sellers {
  margin-bottom: 36px;
}
</style>
