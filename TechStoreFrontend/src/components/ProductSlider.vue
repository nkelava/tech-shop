<script setup>
import { Carousel, Slide, Navigation } from "vue3-carousel";
import "vue3-carousel/dist/carousel.css";
import ProductCard from "@/components/ProductCard.vue";

const props = defineProps(["header", "products"]);
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
    itemsToShow: 2,
    snapAlign: "center",
  },
  // 1024 and up
  1024: {
    itemsToShow: 3,
    snapAlign: "start",
  },
  1400: {
    itemsToShow: 4,
    snapAlign: "start",
  },
  1800: {
    itemsToShow: 5,
    snapAlign: "start",
  },
};
</script>
<template>
  <div class="carousel-container">
    <h2>{{ props.header }}</h2>
    <hr />
    <carousel :items-to-show="4.5" :wrap-around="true" :breakpoints="breakpoints">
      <slide v-for="product in props.products" :key="product">
        <product-card :product="product" />
      </slide>

      <template #addons>
        <navigation />
      </template>
    </carousel>
  </div>
</template>

<style scoped>
.carousel-container {
  max-width: 70%;
  margin: 0 auto;
}

.carousel__pagination {
  margin-top: 1.5rem;
}

hr {
  border: 1px solid var(--ts-c-primary-darker);
  border-radius: 5px;
  margin-bottom: 2rem;
}
</style>
