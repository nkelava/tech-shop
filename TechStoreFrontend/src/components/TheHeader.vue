<script setup>
import { RouterLink } from "vue-router";
import EmailIcon from "@/assets/icons/contact/gmail.png";
import PhoneIcon from "@/assets/icons/contact/phone.png";
import UserIcon from "@/assets/icons/header/user.png";
import SearchIcon from "@/assets/icons/header/search.png";
import MenuIcon from "@/assets/icons/header/menu.png";
import FavoriteIcon from "@/assets/icons/header/favorite.png";
import CartIcon from "@/assets/icons/header/cart.png";

const userEmail = "test@gmail.com";
const counter = 3;
</script>

<template>
  <div class="header">
    <!-- CONTACT INFO AND LOGIN/REGISTER -->
    <div class="header-top">
      <div class="header-contact">
        <div class="contact-item">
          <img :src="EmailIcon" alt="email icon" class="header-icon" />
          <a href="techshop@gmail.com"> Email: techshop@gmail.com </a>
        </div>
        <p class="separator">|</p>
        <div class="contact-item">
          <img :src="PhoneIcon" alt="phone icon" class="header-icon" />
          <p>Phone: 123-456-7890</p>
        </div>
      </div>
      <div class="header-auth">
        <router-link to="/" class="link-p">Login</router-link>
        <p class="separator">|</p>
        <router-link to="/" class="link-p">Register</router-link>
      </div>
    </div>

    <!-- SHOP LOGO, SEARCH BAR AND USER GREETING/ICON -->
    <div class="header-middle">
      <div class="header-brand">
        <h1>TechShop</h1>
      </div>
      <div class="header-search">
        <input type="text" placeholder="Search..." />
        <button class="search-btn">
          <img :src="SearchIcon" alt="phone icon" class="header-icon" />
        </button>
      </div>
      <div class="header-greeting">
        <p>Hello, {{ userEmail }}</p>
        <img :src="UserIcon" alt="email icon" class="user-icon" />
      </div>
    </div>

    <!-- CATEGORIES, FAVORITES, CART -->
    <div class="header-bottom">
      <div>
        <button class="categories-btn">
          <img :src="MenuIcon" alt="menu icon" class="header-icon" />
          <span>All Categories</span>
        </button>
      </div>
      <div class="categories">
        <router-link class="categories-link" to="/">Laptops</router-link>
        <router-link class="categories-link" to="/">Computers</router-link>
        <router-link class="categories-link" to="/">Monitors</router-link>
        <router-link class="categories-link" to="/">Peripherals</router-link>
        <router-link class="categories-link" to="/">Memory</router-link>
        <router-link class="categories-link" to="/">Contact Us</router-link>
      </div>
      <div>
        <button class="btn">
          <img :src="FavoriteIcon" alt="favorites icon" class="header-icon" />
        </button>
        <button class="btn cart-btn">
          <img :src="CartIcon" alt="cart icon" class="header-icon" />
        </button>
        <span class="counter">
          {{ counter }}
        </span>
      </div>
    </div>
  </div>
</template>

<style scoped>
.header-top,
.header-middle,
.header-bottom {
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  padding: 0.5rem 2rem;
}

.header-top {
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-header-dark);
}

.header-contact a {
  color: var(--ts-c-header-dark);
}

.header-contact,
.header-auth {
  display: flex;
}

.contact-item {
  display: flex;
  align-items: center;
}

.separator {
  margin: 0 1rem;
}

.link-p {
  color: var(--ts-c-text-dark);
}

.header-icon {
  padding: 0 6px;
}

.user-icon {
  padding-left: 6px;
}

.header-middle {
  color: var(--ts-c-text-light);
}

.header-search,
.header-greeting {
  position: relative;
  display: flex;
  align-items: center;
}

.header-search {
  width: 500px;
  height: 25px;
}

.header-search input {
  border-radius: 30px;
  padding-left: 1rem;
  width: 500px;
  height: 100%;
  border: none;
}

.search-btn {
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

.header-bottom {
  color: var(--ts-c-text-light);
}

.categories-btn {
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
  border: none;
  border-radius: 30px;
  padding: 5px 30px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.categories-btn:hover {
  text-decoration: underline;
}

.categories-link {
  color: var(--ts-c-text-light);
  margin: 0 10px;
}

.btn {
  border: none;
  background: transparent;
}

.counter {
  border: 1px solid var(--ts-c-border-light);
  border-radius: 50%;
  padding: 3px;
  vertical-align: top;
}
</style>
