<script setup>
import { axiosPublic } from "@/api/axios";
import { onMounted, ref } from "vue";
import ImageSlider from "@/components/ImageSlider.vue";
import TheBenefits from "@/components/TheBenefits.vue";
import TheBanners from "@/components/TheAds.vue";
import ProductGrid from "@/components/ProductGrid.vue";
import ProductSlider from "@/components/ProductSlider.vue";

const bestSellerProducts = ref([]);
const newArrivalProducts = ref([]);
const hotOfferProducts = ref([]);
const topRatedProducts = ref([]);
const activeTab = ref("new");

onMounted(async () => {
  bestSellerProducts.value = await axiosPublic
    .get("/products/bestsellers")
    .then((response) => response.data)
    .catch((error) => {
      console.error(`Failed to fetch bestsellers:`, error);
      bestSellerProducts.value = [];
    });

  newArrivalProducts.value = await axiosPublic
    .get("/products/new")
    .then((response) => response.data)
    .catch((error) => {
      console.error(`Failed to fetch new arrivals:`, error);
      newArrivalProducts.value = [];
    });

  hotOfferProducts.value = await axiosPublic
    .get("/products/hot-offers")
    .then((response) => response.data)
    .catch((error) => {
      console.error(`Failed to fetch hot offers:`, error);
      hotOfferProducts.value = [];
    });

  topRatedProducts.value = await axiosPublic
    .get("/products/top")
    .then((response) => response.data)
    .catch((error) => {
      console.error(`Failed to fetch top rated products:`, error);
      topRatedProducts.value = [];
    });
});
</script>

<template>
  <div>
    <image-slider />
    <the-benefits />

    <div class="best-sellers ts-container">
      <h2>Best Sellers</h2>
      <hr />
      <template v-if="bestSellerProducts && bestSellerProducts.length">
        <product-grid :products="bestSellerProducts" />
      </template>
      <p v-else>There are no best sellers available at the moment.</p>
    </div>

    <the-banners />

    <div class="ts-container tabs-container">
      <div class="tabs">
        <v-tabs v-model="activeTab">
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
        <v-window v-model="activeTab" class="overflow-visible">
          <v-window-item value="new">
            <template v-if="newArrivalProducts && newArrivalProducts.length">
              <product-slider :products="newArrivalProducts" />
            </template>
            <p v-else>No new arrivals available at the moment.</p>
          </v-window-item>

          <v-window-item value="hot">
            <template v-if="hotOfferProducts && hotOfferProducts.length">
              <product-slider :products="hotOfferProducts" />
            </template>
            <p v-else>No hot offers available at the moment.</p>
          </v-window-item>

          <v-window-item value="top">
            <template v-if="topRatedProducts && topRatedProducts.length">
              <product-slider :products="topRatedProducts" />
            </template>
            <p v-else>No top-rated products available at the moment.</p>
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

.tabs-container p {
  font-size: 1rem;
}

:deep(.v-divider--vertical) {
  display: none;
}

:deep(.tabs-container .carousel__slide) {
  padding: 10px 0 !important;
}

@media only screen and (min-width: 48em) {
  .tabs button {
    font-size: 1rem;
  }

  :deep(.v-divider--vertical) {
    display: inline;
    border: 1px solid var(--ts-c-secondary);
    border-radius: 5px;
  }
}
</style>
