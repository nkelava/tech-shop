<script setup>
import { ref, toRefs } from "vue";
import "vue3-carousel/dist/carousel.css";
import DefaultImage from "@/assets/images/test/products/defaultProductImage.png";

const props = defineProps(["product"]);
const { product } = toRefs(props);
const currentSlide = ref(0);
const slideTo = (val) => {
  currentSlide.value = val;
};
</script>

<template>
  <section class="gallery-container">
    <div class="carousel__item">
      <img
        :src="
          product?.imageURL
            ? product.imageURL
            : product?.imageByte
            ? `data:image/jpeg;base64,` + product?.imageByte
            : DefaultImage
        "
        class="gallery__img"
        alt="default product image"
      />
    </div>
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
  border-radius: 5px;
}

#gallery img,
.carousel__item img,
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
