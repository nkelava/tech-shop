<script setup>
import TheCard from "@/components/TheCard.vue";
import DellAdImage from "../assets/images/test/banners/dell.jpg";
import LogitechAdImage from "../assets/images/test/banners/logitech.jpg";
import AsusAdImage from "../assets/images/test/banners/asus.png";
</script>

<template>
  <div>
    <div class="hero">
      <img src="" alt="" />
    </div>
    <!-- TODO: separate components - banner list, banner item -->
    <div class="banner-container">
      <div class="banner-item">
        <h3>Lorem ipsum</h3>
        Lorem, ipsum.
      </div>
      <div class="banner-item">
        <h3>Lorem ipsum</h3>
        Lorem, ipsum.
      </div>
      <div class="banner-item">
        <h3>Lorem ipsum</h3>
        Lorem, ipsum.
      </div>
      <div class="banner-item">
        <h3>Lorem ipsum</h3>
        Lorem, ipsum.
      </div>
    </div>
    <!-- ----------------------------------------------------- -->
    <!-- Best sellers - TODO: product list could be a separate component -->
    <div class="best-sellers">
      <h3>Best Sellers</h3>
      <hr />
      <TheCard />
    </div>
    <!-- ------------ -->
    <!-- Add section -->
    <div class="ads">
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
    <!-- Top rated, New arrivals, Hot offers - TODO: product list could be a separate component -->
    <div>
      <div class="topnewhot">
        <div class="links">
          <a href="#">Top Rated</a>
          |
          <a href="#">New Arrivals</a>
          |
          <a href="#">Hot Offer</a>
        </div>
        <hr />
        <TheCard />
      </div>
    </div>
    <!-- ----------------------------------- -->
  </div>
</template>

<style scoped>
.hero {
  background: var(--my-s);
  height: 500px;
}

.banner-container {
  width: 100%;
  margin-top: 60px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.banner-item {
  background-color: var(--my-s);
  color: var(--my-p);
  border: 1px solid var(--my-p);
  padding: 1rem 2rem;
}

.banner-item:first-child {
  border-radius: 10px 0 0 10px;
}

.banner-item:last-child {
  border-radius: 0 10px 10px 0;
}

.best-sellers,
.topnewhot {
  margin: 40px 0;
  padding: 0.5rem 5rem;
}

.best-sellers h3 {
  color: var(--my-t);
}

.ads {
  background-color: var(--my-s);
  height: 500px;
  width: 100%;
  display: flex;
  margin: 2rem 0;
  padding: 2rem;
}

.left,
.top,
.bottom {
  background-color: #847c5f;
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
  color: var(--my-t);
}

.links a {
  color: #cabb9d;
  text-decoration: none;
  font-size: 1rem;
  margin: 0 0.5rem;
}
</style>
