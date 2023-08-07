<script setup>
import { RouterLink } from "vue-router";
import TheNavBar from "@/components/layout/TheNavBar.vue";
import SearchInput from "@/components/common/SearchInput.vue";
import WishlistDialog from "@/components/wishlist/WishlistDialog.vue";
import CartDialog from "@/components/cart/CartDialog.vue";
import UserProfileDropdown from "@/components/UserProfileDropdown.vue";
import BrandLogo from "@/assets/icons/header/brand-logo.svg";

// TODO: implement user state
const isSignedIn = true;
</script>

<template>
  <header class="ts-container header">
    <div class="header-top">
      <h1 class="header__brand">
        <router-link to="/">
          <img class="header__brand-icon" :src="BrandLogo" alt="tech planet logo" />
        </router-link>
      </h1>
      <search-input class="header__search" />
    </div>
    <div class="header__widgets">
      <the-nav-bar />
      <div class="d-flex">
        <wishlist-dialog />
        <cart-dialog />
        <user-profile-dropdown v-if="isSignedIn" />
      </div>
    </div>
  </header>
</template>

<style scoped>
.header {
  position: relative;
  display: flex;
  flex-direction: column;
  gap: 1rem;
  padding: 1rem 1rem 0;
  align-items: flex-start;
  color: var(--ts-c-text-light);
  border-bottom: 1px solid var(--ts-c-primary-darker);
}

.header__brand a {
  display: block;
}

.header__brand-icon {
  width: 100%;
  height: 100%;
  max-height: 50px;
  vertical-align: middle;
}

.header__search,
.header__widgets {
  width: 100%;
}

.header__widgets {
  display: flex;
  justify-content: space-between;
}

.header-top {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  align-items: flex-start;
  width: 100%;
}

@media only screen and (min-width: 48em) {
  .header-top {
    flex-direction: row;
    justify-content: space-between;
  }
}

@media only screen and (min-width: 64em) {
  .header {
    padding-left: 4.5rem;
    padding-right: 4.5rem;
    padding-bottom: 0;
  }
}
</style>
