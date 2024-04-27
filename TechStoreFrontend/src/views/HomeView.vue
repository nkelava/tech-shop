<script setup>
import { axiosPublic } from "@/api/axios";
import { onMounted, ref } from "vue";
import ImageSlider from "@/components/ImageSlider.vue";
import TheBenefits from "@/components/TheBenefits.vue";
import TheBanners from "@/components/TheAds.vue";
import ProductGrid from "@/components/ProductGrid.vue";
import ProductSlider from "@/components/ProductSlider.vue";

const bestSellers = ref([]);
const newArrivals = ref([]);
const hotOffers = ref([]);
const topRated = ref([]);
const tab = ref("new");

onMounted(async () => {
  bestSellers.value = await axiosPublic
    .get("/products/bestsellers")
    .then((response) => response.data)
    .catch(() => null);

  newArrivals.value = await axiosPublic
    .get("/products/new")
    .then((response) => response.data)
    .catch(() => null);

  hotOffers.value = await axiosPublic
    .get("/products/top")
    .then((response) => response.data)
    .catch(() => null);

  topRated.value = await axiosPublic
    .get("/products/top")
    .then((response) => response.data)
    .catch(() => null);
});
</script>

<template>
  <div>
    <image-slider />
    <the-benefits />
    <div class="best-sellers ts-container">
      <h2>Best Sellers</h2>
      <hr />
      <product-grid v-if="bestSellers" :products="bestSellers" />
    </div>
    <the-banners />
    <div class="ts-container tabs-container">
      <div class="tabs">
        <v-tabs v-model="tab">
          <v-tab value="new">
            <h3 class="text-capitalize">New Arrivals</h3>
          </v-tab>
          <v-divider class="mx-3" inset vertical></v-divider>
          <v-tab value="hot">
            <h3 class="text-capitalize">Hot Offers</h3>
          </v-tab>
          <v-divider class="mx-3" inset vertical></v-divider>
          <v-tab value="top">
            <h3 class="text-capitalize">Top Rated</h3>
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
.tabs-container {
  height: 100%;
  min-height: 660px;
}

.tabs {
  margin-bottom: 1rem;
}

.tabs button {
  font-size: 12px;
}

::v-deep .v-divider--vertical {
  display: none;
}

::v-deep .tabs-container .carousel__slide {
  padding: 10px 0 !important;
}

@media only screen and (min-width: 48em) {
  .tabs button {
    font-size: 1rem;
  }

  ::v-deep .v-divider--vertical {
    display: inline;
    border: 1px solid var(--ts-c-secondary);
    border-radius: 5px;
  }
}
</style>
