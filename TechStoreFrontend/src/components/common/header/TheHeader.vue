<script setup>
import { RouterLink } from "vue-router";
import FavoritesModal from "@/components/common/header/favorites/FavoritesModal.vue";
import CartModal from "@/components/common/header/cart/CartModal.vue";
import UserProfileDropdown from "@/components/common/header/account/UserProfileDropdown.vue";
import TheNavbar from "@/components/common/header/TheNavbar.vue";

import EmailIcon from "@/assets/icons/contact/gmail16.png";
import PhoneIcon from "@/assets/icons/contact/phone16.png";
import SearchIcon from "@/assets/icons/header/search.png";
</script>

<template>
  <div class="header">
    <!-- CONTACT INFO AND LOGIN/REGISTER -->
    <!-- this is called top bar  -->
    <div class="header__info">
      <div class="info__contacts">
        <img :src="EmailIcon" class="contact__icon" alt="email icon" />
        <a href="techshop@gmail.com" class="contact__text"> Email: techshop@gmail.com </a>
        <p class="separator">|</p>
        <img :src="PhoneIcon" alt="phone icon" class="contact__icon" />
        <p class="contact__text">Phone: 123-456-7890</p>
      </div>
      <div class="info__auth">
        <router-link to="/" class="contact__text">Register</router-link>
        <p class="separator">|</p>
        <router-link to="/login" class="contact__text">Login</router-link>
      </div>
    </div>

    <!-- SHOP LOGO, SEARCH BAR AND USER GREETING/ICON -->
    <div class="header__main">
      <div class="header__brand">
        <h1>TechShop</h1>
      </div>
      <div class="header__search">
        <input class="search__input" type="text" placeholder="Search..." />
        <button class="search__btn">
          <img :src="SearchIcon" alt="phone icon" class="search__icon" />
        </button>
      </div>
      <div class="header__dropdowns">
        <favorites-modal />
        <cart-modal />
        <UserProfileDropdown />
      </div>
    </div>
    <TheNavbar class="header__nav" />
  </div>
</template>

<style scoped>
.header__info,
.header__main,
.header__nav {
  color: var(--ts-c-text-light);
  display: flex;
  flex-wrap: wrap;
  align-items: center;
}

.header__info,
.header__main {
  justify-content: space-between;
  padding: 0.5rem 2rem;
}

.header__info {
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
}

.info__contacts,
.info__auth {
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.separator {
  margin: 0 1rem;
}

.contact__text {
  color: var(--ts-c-text-dark);
}

.dropdown__icon {
  margin-inline: 0.5rem;
}

.header__search {
  position: relative;
  display: flex;
  align-items: center;
  width: 500px;
  height: 25px;
}

.search__input {
  border-radius: 30px;
  padding-left: 1rem;
  width: 500px;
  height: 100%;
  border: none;
}

.search__btn {
  background-color: var(--ts-c-bg-light);
  border: none;
  border-radius: 30px;
  width: 3rem;
  height: 25px;
  position: absolute;
  right: 0;
}

.search-btn img {
  vertical-align: middle;
}

.header__dropdowns {
  display: flex;
}

.dropdown__btn {
  border: none;
  background: transparent;
}
</style>
