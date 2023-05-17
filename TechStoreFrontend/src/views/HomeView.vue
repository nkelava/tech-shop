<script setup>
import TheSlider from "@/components/TheSlider.vue";
import TheBenefitList from "@/components/TheBenefitList.vue";
import TheBanners from "@/components/TheBanners.vue";
import TheNewsletters from "@/components/TheNewsletters.vue";

import AppProductList from "@/components/AppProductList.vue";
import AppProductSlider from "../components/AppProductSlider.vue";
import { products } from "@/data/products.js";
</script>

<template>
  <div>
    <TheSlider />
    <TheBenefitList />
    <div class="best-sellers">
      <!-- TODO: add product grid component and pagination to it -->
      <h2>Best Sellers</h2>
      <hr />
      <AppProductList :products="products" />
    </div>
    <TheBanners />
    <!-- Add product slider component  -->
    <div class="new">
      <h2>New Arrivals</h2>
      <hr />
      <AppProductSlider :products="products" />
    </div>
    <div class="top">
      <h2>Top Rated</h2>
      <hr />
      <AppProductSlider :products="products" />
    </div>
    <!-- Newsletters section -->
    <TheNewsletters />
    <!-- ------------------- -->
  </div>
</template>

<style scoped>
.best-sellers {
  margin: 40px 0;
  padding: 0.5rem 5rem;
}

.best-sellers h2,
.new h2,
.top h2 {
  color: var(--ts-c-text-highlight);
}

hr {
  margin-bottom: 2rem;
}

.new,
.top {
  margin: 40px 0;
  padding: 0.5rem 5rem;
}
</style>
