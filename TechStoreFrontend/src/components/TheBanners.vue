<script setup>
import { DellImage, LogitechImage, AsusImage } from "@/assets/images/test/banners";
</script>

<template>
  <div class="banners">
    <img class="banners__img" :src="DellImage" alt="" />
    <img class="banners__img" :src="LogitechImage" alt="" />
    <img class="banners__img" :src="AsusImage" alt="" />
  </div>
</template>

<style scoped>
.banners {
  background-color: var(--ts-c-bg-light);
  display: grid;
  grid-template-areas:
    "first second"
    "first third";
  gap: 1rem;
  justify-content: center;
  height: 40em;
  padding: 3rem;
}

.banners__img:nth-child(1) {
  grid-area: first;
}

.banners__img:nth-child(2) {
  grid-area: second;
}

.banners__img:nth-child(3) {
  grid-area: third;
}

.banners__img {
  border-radius: 10px;
  box-shadow: 5px 5px 5px var(--ts-c-primary-darker);
  width: 100%;
  min-width: 350px;
  max-width: 1000px;
  height: 100%;
}

@media only screen and (max-width: 800px) {
  .banners {
    grid-template-areas:
      "first"
      "second"
      "third"
      "fourth";
    padding-inline: 5rem;
  }
}
</style>
