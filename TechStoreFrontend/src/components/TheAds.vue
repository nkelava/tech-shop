<script setup>
import { DellImage, LogitechImage, AsusImage } from "@/assets/images/test/banners";
</script>

<template>
  <div class="ads ts-container">
    <img class="ads__img" :src="DellImage" alt="" />
    <img class="ads__img" :src="LogitechImage" alt="" />
    <img class="ads__img" :src="AsusImage" alt="" />
  </div>
</template>

<style scoped>
.ads {
  display: grid;
  grid-template-areas:
    "first"
    "second"
    "third"
    "fourth";
  gap: 1rem;
  background-color: var(--ts-c-bg-light);
}

.ads__img:nth-child(1) {
  grid-area: first;
}

.ads__img:nth-child(2) {
  grid-area: second;
}

.ads__img:nth-child(3) {
  grid-area: third;
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

@media only screen and (min-width: 64em) {
}
</style>
