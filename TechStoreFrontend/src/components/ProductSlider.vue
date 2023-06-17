<script setup>
import { Carousel, Slide, Pagination, Navigation } from "vue3-carousel";
import "vue3-carousel/dist/carousel.css";
import ProductCard from "@/components/ProductCard.vue";

const { products } = defineProps(["products"]);
const breakpoints = {
  400: {
    itemsToShow: 1,
    snapAlign: "center",
  },
  // 700px and up
  700: {
    itemsToShow: 2,
    snapAlign: "center",
  },
  900: {
    itemsToShow: 3.5,
    snapAlign: "center",
  },
  // 1024 and up
  1024: {
    itemsToShow: 4,
    snapAlign: "start",
  },
  1400: {
    itemsToShow: 5,
    snapAlign: "start",
  },
  1800: {
    itemsToShow: 6,
    snapAlign: "start",
  },
};
</script>
<template>
  <carousel :items-to-show="5.5" :wrap-around="true" :breakpoints="breakpoints">
    <slide v-for="product in products" :key="product">
      <product-card :product="product" />
    </slide>

    <template #addons>
      <navigation />
      <pagination />
    </template>
  </carousel>
</template>

<style scoped>
.carousel__pagination {
  margin-top: 1.5rem;
}
</style>
