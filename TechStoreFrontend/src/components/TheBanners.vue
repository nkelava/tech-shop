<script setup>
import DellAdImage from "@/assets/images/test/banners/dell.jpg";
import LogitechAdImage from "@/assets/images/test/banners/logitech.jpg";
import AsusAdImage from "@/assets/images/test/banners/asus.png";
</script>

<template>
  <div class="banners">
    <img :src="DellAdImage" alt="" />
    <img :src="LogitechAdImage" alt="" />
    <img :src="LogitechAdImage" alt="" />
    <img :src="AsusAdImage" alt="" />
  </div>
</template>

<style scoped>
.banners {
  background-color: var(--ts-c-bg-light);
  height: 40em;
  padding: 3rem;
  display: grid;
  grid-template-areas:
    "first second"
    "third fourth";
  gap: 1rem;
}

.banners:nth-child(1) {
  grid-area: first;
}

.banners:nth-child(2) {
  grid-area: second;
}

.banners:nth-child(3) {
  grid-area: third;
}

.banners:nth-child(4) {
  grid-area: fourth;
}

.banners img {
  border-radius: 1rem;
  width: 100%;
  height: 100%;
}

@media only screen and (min-width: 1400px) {
  .banners {
    padding-inline: 10rem;
  }
}

@media only screen and (min-width: 500px) {
  .banners {
    padding-inline: 7rem;
  }
}

@media only screen and (max-width: 750px) {
  .banners {
    grid-template-areas:
      "first"
      "second"
      "third"
      "fourth";
  }
}
</style>
