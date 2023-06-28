<script setup>
import BenefitCard from "@/components/BenefitCard.vue";
import HomeIcon from "../assets/icons/benefits/house.png";
import RefundIcon from "../assets/icons/benefits/refund.png";
import SupportIcon from "../assets/icons/benefits/shield.png";
import RocketIcon from "../assets/icons/benefits/rocket.png";
</script>

<template>
  <div class="benefits-container">
    <benefit-card
      :icon="HomeIcon"
      title="Home Delivery"
      content="Get your products delivered straight to your door, saving you time and effort."
    />
    <benefit-card
      :icon="RocketIcon"
      title="Fast Delivery"
      content="Experience prompt shipping and receive your items swiftly, ensuring timely satisfaction."
    />
    <benefit-card
      :icon="RefundIcon"
      title="100% Refund"
      content="Rest easy knowing that we offer a complete refund, ensuring your peace of mind."
    />
    <benefit-card
      :icon="SupportIcon"
      title="Client Support"
      content="Access round-the-clock client support to assist you whenever you need assistance."
    />
  </div>
</template>

<style scoped>
.benefits-container {
  display: grid;
  grid-template-areas: "home refund support delivery";
  justify-content: center;
  align-content: center;
  margin: 2rem;
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

@media only screen and (max-width: 1600px) {
  .benefits-container {
    grid-template-areas:
      "home refund"
      "support delivery";
    gap: 0.5rem;
  }
}

@media only screen and (max-width: 700px) {
  .benefits-container {
    grid-template-areas:
      "home"
      "refund"
      "support"
      "delivery";
    gap: 0.5rem;
  }
}
</style>
