<script setup>
import { ref } from "vue";
import { Carousel, Slide } from "vue3-carousel";
import "vue3-carousel/dist/carousel.css";
import Helios from "@/assets/images/test/products/helios300.png";
import HeliosL from "@/assets/images/test/products/helios300-lside.png";
import HeliosR from "@/assets/images/test/products/helios300-rside.png";
import HeliosB from "@/assets/images/test/products/helios300-back.png";
import HeliosA from "@/assets/images/test/products/helios300-both.png";

const productImages = [Helios, HeliosL, HeliosR, HeliosB, HeliosA];
const currentSlide = ref(0);
const slideTo = (val) => {
  currentSlide.value = val;
};
</script>

<template>
  <section class="gallery-container">
    <carousel id="gallery" :items-to-show="1" :wrap-around="true" v-model="currentSlide">
      <slide v-for="(image, i) in productImages" :key="i">
        <div class="carousel__item">
          <img :src="image" class="gallery__img" alt="default product image" />
        </div>
      </slide>
    </carousel>

    <carousel
      id="thumbnails"
      :items-to-show="3.5"
      :wrap-around="true"
      v-model="currentSlide"
      ref="carousel"
    >
      <slide v-for="(imageUrl, i) in productImages" :key="i">
        <div class="carousel__item" @click="slideTo(i)">
          <img :src="imageUrl" class="tumbnails__img" alt="default product image" />
        </div>
      </slide>
    </carousel>
  </section>
</template>

<style scoped>
.gallery-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  overflow: hidden;
}

#gallery {
  margin-bottom: 1rem;
  width: 100%;
}

#gallery .carousel__item {
  height: 100%;
  max-height: 400px;
  width: 100%;
  max-width: 400px;
}

img {
  border-radius: 5px;
}

.gallery__img {
  height: 100%;
  width: 100%;
}

#thumbnails {
  width: 100%;
  max-width: 450px;
}

#thumbnails .carousel__item {
  margin-inline: 10px;
  height: 100%;
  max-height: 100px;
  width: 100%;
  max-width: 100px;
}

.tumbnails__img {
  height: 100%;
  width: 100%;
  object-fit: cover;
}
</style>
