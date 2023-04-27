<script setup>
import TheCard from "@/components/TheCard.vue";
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
    <!-- Best sellers -->
    <div class="best-sellers">
      <h3>Best Sellers</h3>
      <hr />
      <TheCard />
    </div>
    <!-- ------------ -->
    <!-- Add section -->
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

.best-sellers {
  margin-top: 40px;
  padding: 0.5rem 5rem;
}

.best-sellers h3 {
  color: var(--my-t);
}
</style>
