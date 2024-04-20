<script setup>
import { Carousel, Slide, Navigation } from "vue3-carousel";
import "vue3-carousel/dist/carousel.css";
import { Skola, SpiderMan, Visa, HyperX } from "@/assets/images/test/carousel";

const slides = [
  {
    img: Skola,
    url: "",
  },
  {
    img: SpiderMan,
    url: "",
  },
  {
    img: Visa,
    url: "",
  },
  {
    img: HyperX,
    url: "",
  },
];
</script>

<template>
  <carousel class="carousel" :autoplay="4000" :wrap-around="true">
    <slide v-for="(slide, i) in slides" :key="i">
      <div class="carousel__item">
        <a :href="slide.url" target="_blank" rel="noopener noreferrer">
          <img :src="slide.img" alt="slide" />
        </a>
      </div>
    </slide>
    <template #addons>
      <navigation class="carousel__nav" />
    </template>
  </carousel>
</template>

<style scoped>
.carousel {
  max-height: 64vh;
}

.carousel__item {
  height: 100%;
  width: 100%;
}

.carousel__item img {
  max-height: 64vh;
  height: 100%;
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
    border-radius: 5px;
  }

  ::v-deep .carousel__prev {
    left: 10px !important;
  }

  ::v-deep .carousel__next {
    right: 15px !important;
  }

  ::v-deep .carousel__prev:hover,
  ::v-deep .carousel__next:hover {
    background-color: rgba(0, 0, 0, 0.2);
  }
}
</style>
