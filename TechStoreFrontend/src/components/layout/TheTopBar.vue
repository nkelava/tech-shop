<script setup>
import { RouterLink } from "vue-router";
import ContactInfo from "@/components/common/ContactInfo.vue";
import EmailIcon from "@/assets/icons/contact/gmail16.png";
import PhoneIcon from "@/assets/icons/contact/phone16.png";

// TODO: implement user state
const isSignedIn = false;
</script>

<template>
  <div class="header-top ts-container">
    <div class="header-top__contact">
      <contact-info :imgUrl="EmailIcon" imgAlt="email icon">
        Email: tech-store@gmail.com
      </contact-info>
      <contact-info :imgUrl="PhoneIcon" imgAlt="phone icon"> Phone: 123-456-7890 </contact-info>
    </div>
    <div v-if="!isSignedIn" class="header-top__auth">
      <v-icon icon="mdi-login-variant"></v-icon>
      <router-link to="/auth">Sign In / Sign Up</router-link>
    </div>
  </div>
</template>

<style scoped>
.header-top {
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  justify-content: space-between;
  font-size: var(--ts-text-size-sm);
}

.header-top__contact {
  display: none;
}

@media only screen and (min-width: 40em) {
  .header-top__contact,
  .header-top__auth {
    display: flex;
    align-items: center;
    gap: 0.5rem;
  }
}
</style>
