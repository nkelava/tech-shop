<script setup>
import AppProductCard from "@/components/AppProductCard.vue";
import "vue3-carousel/dist/carousel.css";
import { Carousel, Slide, Pagination, Navigation } from "vue3-carousel";

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
  <Carousel :items-to-show="5.5" :wrap-around="true" :breakpoints="breakpoints">
    <Slide v-for="product in products" :key="product">
      <AppProductCard :product="product" />
    </Slide>

    <template #addons>
      <Navigation />
      <Pagination />
    </template>
  </Carousel>
</template>

<style scoped>
.carousel__pagination {
  margin-top: 1rem;
}
</style>
