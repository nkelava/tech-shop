<script setup>
import BenefitCard from "@/components/BenefitCard.vue";

import HomeIcon from "../assets/icons/benefits/house.png";
import RefundIcon from "../assets/icons/benefits/refund.png";
import SupportIcon from "../assets/icons/benefits/shield.png";
import RocketIcon from "../assets/icons/benefits/rocket.png";
</script>

<template>
  <div class="benefits-container">
    <BenefitCard :icon="HomeIcon" title="Home Shipping" text="Lorem, ipsum." />
    <BenefitCard :icon="RefundIcon" title="100% Refund" text="Lorem, ipsum." />
    <BenefitCard :icon="SupportIcon" title="Client Support" text="Lorem, ipsum." />
    <BenefitCard :icon="RocketIcon" title="Fast Delivery" text="Lorem, ipsum." />
  </div>
</template>

<style scoped>
.benefits-container {
  margin-top: 2rem;
  display: grid;
  grid-template-areas: "home refund support delivery";
  justify-content: center;
}

.benefits-container:nth-child(1) {
  grid-area: home;
}

.benefits-container:nth-child(2) {
  grid-area: refund;
}

.benefits-container:nth-child(3) {
  grid-area: support;
}

.benefits-container:nth-child(4) {
  grid-area: delivery;
}

@media only screen and (max-width: 1200px) {
  .benefits-container {
    gap: 0.5rem;
    grid-template-areas:
      "home refund"
      "support delivery";
  }
}

@media only screen and (max-width: 600px) {
  .benefits-container {
    gap: 0.5rem;
    grid-template-areas:
      "home"
      "refund"
      "support"
      "delivery";
  }
}
</style>
