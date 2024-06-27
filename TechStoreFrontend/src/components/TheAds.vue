<script setup>
import { AsusImage, DellImage, LogitechImage } from "@/assets/images/test/banners";
</script>

<template>
  <div class="ads ts-container">
    <img class="ads__img" :src="DellImage" alt="dell-ad" />
    <img class="ads__img" :src="LogitechImage" alt="logitech-ad" />
    <img class="ads__img" :src="AsusImage" alt="asus-ad" />
  </div>
</template>

<style scoped>
.ads {
  display: grid;
  grid-template-areas:
    "first"
    "second"
    "third";
  gap: 1rem;
  justify-content: center;
  background: url("/src/assets/images/test/banners/ads_background.jpg"), var(--ts-c-bg-light);
}

.ads__img:nth-child(1) {
  grid-area: first;
}

.ads__img:nth-child(2) {
  grid-area: second;
  max-height: 300px;
}

.ads__img:nth-child(3) {
  grid-area: third;
  max-height: 300px;
}

.ads__img {
  border-radius: 10px;
  box-shadow: 5px 5px 5px var(--ts-c-primary-darker);
  width: 100%;
  min-width: 250px;
  max-width: 1000px;
  height: 100%;
  min-height: 200px;
}

@media only screen and (min-width: 48em) {
  .ads {
    grid-template-areas:
      "first second"
      "first third";
  }
}
</style>
