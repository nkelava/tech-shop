<script setup>
import { Carousel, Navigation, Slide } from "vue3-carousel";
import "vue3-carousel/dist/carousel.css";
import Skola from "@/assets/images/test/carousel/skola.webp";
import SpiderMan from "@/assets/images/test/carousel/spider-man.webp";
import Visa from "@/assets/images/test/carousel/visa.webp";
import Hyperx from "@/assets/images/test/carousel/hyperx.jpg";

// TODO: either separate hero slider from this or add images as props
const images = [Skola, SpiderMan, Visa, Hyperx];
const props = defineProps(["maxHeight"]);
</script>

<template>
  <carousel class="carousel" :autoplay="5000" :wrap-around="true">
    <slide v-for="(image, i) in images" :key="i">
      <div class="carousel__item">
        <img :src="image" alt="" />
      </div>
    </slide>

    <template #addons>
      <navigation />
    </template>
  </carousel>
</template>

<style scoped>
.carousel {
  min-height: 200px;
  max-height: v-bind("props.maxHeight");
}

.carousel__item {
  height: 100%;
  width: 100%;
}

.carousel__item img {
  height: 100%;
  max-height: v-bind("props.maxHeight");
  width: 100%;
}
</style>
