<script setup>
import { Carousel, Slide } from "vue3-carousel";
import "vue3-carousel/dist/carousel.css";
import { Skola, SpiderMan, Visa, HyperX } from "@/assets/images/test/carousel";

const images = [Skola, SpiderMan, Visa, HyperX];
const props = defineProps(["maxHeight"]);
</script>

<template>
  <carousel class="carousel" :autoplay="4000" :wrap-around="true">
    <slide v-for="(image, i) in images" :key="i">
      <div class="carousel__item">
        <img :src="image" alt="" />
      </div>
    </slide>
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
