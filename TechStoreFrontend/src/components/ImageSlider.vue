<script setup>
import { Carousel, Slide, Navigation } from "vue3-carousel";
import "vue3-carousel/dist/carousel.css";
import { Skola, SpiderMan, Visa, HyperX } from "@/assets/images/test/carousel";

const images = [Skola, SpiderMan, Visa, HyperX];
</script>

<template>
  <carousel class="carousel" :autoplay="4000" :wrap-around="true">
    <slide v-for="(image, i) in images" :key="i">
      <div class="carousel__item">
        <img :src="image" alt="" />
      </div>
    </slide>
    <template #addons>
      <navigation class="carousel__nav" />
    </template>
  </carousel>
</template>

<style scoped>
.carousel {
  min-height: 64vh;
}

.carousel__item {
  height: 100%;
  width: 100%;
}

.carousel__item img {
  height: 64vh;
  width: 100%;
}

::v-deep .carousel__prev,
::v-deep .carousel__next {
  display: none !important;
}

@media only screen and (min-width: 48em) {
  ::v-deep .carousel__prev,
  ::v-deep .carousel__next {
    display: flex !important;
    color: var(--ts-c-bg-dark) !important;
  }
  ::v-deep .carousel__prev {
    left: 10px !important;
  }

  ::v-deep .carousel__next {
    right: 15px !important;
  }
}
</style>
