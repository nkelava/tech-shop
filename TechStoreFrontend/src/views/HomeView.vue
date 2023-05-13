<script setup>
import AppCard from "@/components/AppCard.vue";
import TheSlider from "@/components/TheSlider.vue";
import TheBenefitList from "@/components/TheBenefitList.vue";
import TheNewsletters from "@/components/TheNewsletters.vue";

import DellAdImage from "../assets/images/test/banners/dell.jpg";
import LogitechAdImage from "../assets/images/test/banners/logitech.jpg";
import AsusAdImage from "../assets/images/test/banners/asus.png";
</script>

<template>
  <div>
    <TheSlider />
    <TheBenefitList />
    <!-- Best sellers section - TODO: product list could be a separate component -->
    <div class="best-sellers">
      <h3>Best Sellers</h3>
      <hr />
      <AppCard />
    </div>
    <!-- ------------ -->
    <!-- Promotions section -->
    <div class="promotions">
      <div class="left">
        <img :src="DellAdImage" alt="" />
      </div>
      <div class="right">
        <div class="top">
          <img :src="AsusAdImage" alt="" />
        </div>
        <div class="bottom">
          <img :src="LogitechAdImage" alt="" />
        </div>
      </div>
    </div>
    <!-- ----------- -->
    <!-- Top rated, New arrivals, Hot offers section - TODO: product list could be a separate component -->
    <div class="topnewhot">
      <div class="links">
        <a href="#">Top Rated</a>
        |
        <a href="#">New Arrivals</a>
        |
        <a href="#">Hot Offer</a>
      </div>
      <hr />
      <AppCard />
    </div>
    <!-- ----------------------------------- -->
    <!-- Newsletters section -->
    <TheNewsletters />
    <!-- ------------------- -->
  </div>
</template>

<style scoped>
.best-sellers,
.topnewhot {
  margin: 40px 0;
  padding: 0.5rem 5rem;
}

.best-sellers h3 {
  color: var(--ts-c-text-highlight);
}

.promotions {
  background-color: var(--ts-c-bg-light);
  height: 500px;
  width: 100%;
  display: flex;
  margin: 2rem 0;
  padding: 2rem;
}

.left,
.top,
.bottom {
  background-color: var(--ts-c-secondary-darker);
}

.left,
.right {
  width: 50%;
  margin: 1rem;
}

.right {
  display: block;
}

.top {
  margin-bottom: 40px;
}

.top,
.bottom {
  height: 45%;
}

.left img,
.top img,
.bottom img {
  height: 100%;
  width: 100%;
}

.links a:first-child {
  color: var(--ts-c-text-highlight);
}

.links a {
  color: var(--ts-c-secondary-dark);
  text-decoration: none;
  font-size: 1rem;
  margin: 0 0.5rem;
}
</style>
