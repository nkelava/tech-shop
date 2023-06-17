<script setup>
import { Carousel, Navigation, Slide } from "vue3-carousel";
import Skola from "@/assets/images/test/carousel/skola.webp";
import SpiderMan from "@/assets/images/test/carousel/spider-man.webp";
import Visa from "@/assets/images/test/carousel/visa.webp";
import Hyperx from "@/assets/images/test/carousel/hyperx.jpg";

// TODO: either separate hero slider from this or add images as props
const images = [Skola, SpiderMan, Visa, Hyperx];

// TODO: Fix slider responsiveness
const { height } = defineProps(["height"]);
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
.carousel__item {
  height: 100%;
  max-height: 800px;
  width: 100%;
}

.carousel__item img {
  height: 100%;
  max-height: 800px;
  width: 100%;
}
</style>
