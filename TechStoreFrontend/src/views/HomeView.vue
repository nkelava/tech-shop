<script setup>
import AppCard from "@/components/AppCard.vue";
import TheSlider from "@/components/TheSlider.vue";
import TheBenefitList from "@/components/TheBenefitList.vue";
import TheBanners from "@/components/TheBanners.vue";
import TheNewsletters from "@/components/TheNewsletters.vue";
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
    <TheBanners />
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
