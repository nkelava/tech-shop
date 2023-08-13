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
      <slide class="carousel__slide" v-for="(imageUrl, i) in productImages" :key="i">
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
  margin-bottom: 50px;
  overflow: hidden;
}

#gallery {
  width: 100%;
  margin-bottom: 1rem;
}

#gallery .carousel__item {
  height: 100%;
  max-height: 400px;
  width: 100%;
  max-width: 400px;
}

#gallery img,
#thumbnails img {
  height: 100%;
  width: 100%;
  border-radius: 5px;
}

#thumbnails {
  width: 100%;
  max-width: 450px;
}

#thumbnails .carousel__item {
  height: 100%;
  max-height: 100px;
  width: 100%;
  max-width: 100px;
  margin-inline: 10px;
}

.tumbnails__img {
  object-fit: cover;
}

@media only screen and (min-width: 64em) {
  .gallery-container {
    margin-bottom: 0;
    margin-right: 50px;
  }
}
</style>
