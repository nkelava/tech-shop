<script setup>
import { RouterLink } from "vue-router";
import EmailIcon from "@/assets/icons/contact/gmail16.png";
import PhoneIcon from "@/assets/icons/contact/phone16.png";
import ContactInfo from "@/components/common/ContactInfo.vue";

const isSignedIn = false;
</script>

<template>
  <div class="top-bar">
    <div class="top__contacts">
      <contact-info :imgUrl="EmailIcon" imgAlt="email icon">
        Email: techshop@gmail.com
      </contact-info>
      <v-divider class="mx-2" vertical />
      <contact-info :imgUrl="PhoneIcon" imgAlt="phone icon"> Phone: 123-456-7890 </contact-info>
    </div>
    <div class="top__auth">
      <router-link v-if="!isSignedIn" to="/auth" class="auth__link">Sign In</router-link>
    </div>
  </div>
</template>

<style scoped>
.top-bar {
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  justify-content: space-between;
  padding: 0.5rem 2rem;
}

.top__contacts,
.top__auth {
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.auth__link {
  color: var(--ts-c-text-dark);
}
</style>
