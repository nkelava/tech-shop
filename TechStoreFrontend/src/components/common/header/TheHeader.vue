<script setup>
import { RouterLink } from "vue-router";
import TheTopBar from "./TheTopBar.vue";
import SearchInput from "./SearchInput.vue";
import WishlistDialog from "./wishlist/WishlistDialog.vue";
import CartDialog from "./cart/CartDialog.vue";
import UserProfileDropdown from "./account/UserProfileDropdown.vue";
import TheNavbar from "./TheNavbar.vue";
import BrandLogo from "@/assets/icons/header/brand.png";

// TODO: implement user state
const isSignedIn = true;
</script>

<template>
  <header>
    <!-- CONTACT INFO AND LOGIN/REGISTER -->
    <the-top-bar />
    <!-- SHOP LOGO, SEARCH BAR AND ACTION ICONS -->
    <div class="header__main">
      <router-link to="/">
        <img class="brand-logo" :src="BrandLogo" alt="tech planet logo" />
      </router-link>
      <search-input />
      <div class="d-flex">
        <wishlist-dialog />
        <cart-dialog />
        <user-profile-dropdown v-if="isSignedIn" />
      </div>
    </div>
    <!-- NAVIGATION LINKS -->
    <the-navbar />
  </header>
</template>

<style scoped>
.header__main {
  color: var(--ts-c-text-light);
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  align-items: center;
  padding: 0.5rem 2rem;
}

.brand-logo {
  max-height: 70px;
  max-width: 200px;
}
</style>
