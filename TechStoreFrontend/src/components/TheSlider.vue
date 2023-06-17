<script setup>
import Skola from "@/assets/images/test/carousel/skola.webp";
import SpiderMan from "@/assets/images/test/carousel/spider-man.webp";
import Visa from "@/assets/images/test/carousel/visa.webp";

// TODO: either separate hero slider from this or add images as props
const images = [Skola, SpiderMan, Visa];
const { height } = defineProps(["height"]);
</script>

<template>
  <v-carousel class="carousel" hide-delimiters show-arrows="hover">
    <v-carousel-item v-for="(image, i) in images" :key="i" :src="image" cover></v-carousel-item>
  </v-carousel>
</template>

<style scoped>
.carousel {
  height: v-bind(height) !important;
  width: 100%;
}
</style>
