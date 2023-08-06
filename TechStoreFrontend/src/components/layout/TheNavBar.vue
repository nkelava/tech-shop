<script setup>
import { onMounted, onUnmounted, ref } from "vue";
import { getCategories } from "@/database/services/categoryService.js";

const categories = ref([]);
let isMenuOpened = ref(false);
const windowWidth = ref(window.innerWidth);

onMounted(() => {
  categories.value = getCategories();
  window.addEventListener("resize", handleResize);
});

const toggleMenu = () => {
  isMenuOpened.value = !isMenuOpened.value;
};

const handleResize = () => {
  windowWidth.value = window.innerWidth;

  if (windowWidth.value > 1024) {
    isMenuOpened.value = false;
  }
};

onUnmounted(() => {
  window.removeEventListener("resize", handleResize);
});
</script>

<template>
  <nav class="main-navigation">
    <button
      class="menu-toggle"
      :class="{ open: isMenuOpened }"
      aria-controls="menu-primary"
      aria-expanded="false"
      @click="toggleMenu"
    >
      <span></span>
      <span></span>
      <span></span>
      <span></span>
      <span></span>
      <span></span>
      <span></span>
      <span></span>
      <span></span>
    </button>

    <div class="menu-primary-container" :class="{ show: isMenuOpened }">
      <ul class="menu">
        <li class="menu-item"><a href="/">Home</a></li>
        <li v-for="category in categories" :key="category.id" class="menu-item">
          <a :href="`/${category.slug}`">
            {{ category.name }}
          </a>
        </li>
        <li class="menu-item">
          <a href="/contact">Contact Us</a>
        </li>
      </ul>
    </div>
  </nav>
</template>

<style scoped>
.main-navigation {
  display: flex;
  padding-left: 5px;
}

.main-navigation .menu-toggle {
  position: relative;
  display: flex;
  width: 30px;
  height: 30px;
  background-color: transparent;
  cursor: pointer;
  transition: 0.1s;
}

.main-navigation .menu-toggle span {
  position: absolute;
  display: block;
  width: 5px;
  height: 5px;
  background-color: var(--ts-c-bg-light);
  border-radius: 50%;
}

.main-navigation .menu-toggle span:hover {
  transform: scale(1.2);
  transition: 350ms cubic-bezier(0.8, 0.5, 0.2, 1.4);
}

.main-navigation .menu-toggle span:nth-child(1) {
  left: 0;
  top: 0;
}

.main-navigation .menu-toggle span:nth-child(2) {
  left: 12px;
  top: 0;
}

.main-navigation .menu-toggle span:nth-child(3) {
  right: 0;
  top: 0;
}

.main-navigation .menu-toggle span:nth-child(4) {
  left: 0;
  top: 12px;
}

.main-navigation .menu-toggle span:nth-child(5) {
  position: absolute;
  left: 12px;
  top: 12px;
}

.main-navigation .menu-toggle span:nth-child(6) {
  right: 0px;
  top: 12px;
}

.main-navigation .menu-toggle span:nth-child(7) {
  left: 0px;
  bottom: 0px;
}

.main-navigation .menu-toggle span:nth-child(8) {
  position: absolute;
  left: 12px;
  bottom: 0px;
}

.main-navigation .menu-toggle span:nth-child(9) {
  right: 0px;
  bottom: 0px;
}

.main-navigation .menu-toggle.open {
  cursor: pointer;
  transform: rotate(180deg);
  transition: 0.4s cubic-bezier(0.8, 0.5, 0.2, 1.4);
}

.main-navigation .menu-toggle.open span {
  border-radius: 50%;
  transition-delay: 200ms;
  transition: 0.5s cubic-bezier(0.8, 0.5, 0.2, 1.4);
}

.main-navigation .menu-toggle.open span:nth-child(2) {
  left: 6px;
  top: 6px;
}

.main-navigation .menu-toggle.open span:nth-child(4) {
  left: 6px;
  top: 18px;
}

.main-navigation .menu-toggle.open span:nth-child(6) {
  right: 6px;
  top: 6px;
}

.main-navigation .menu-toggle.open span:nth-child(8) {
  left: 18px;
  bottom: 6px;
}

.main-navigation .menu-primary-container {
  z-index: 99;
  position: absolute;
  top: 100%;
  left: 0;
  height: 70vh;
  width: 100%;
  padding: 50px 50px 50px 0;
  background-color: var(--ts-c-bg-light);
  border: 3px solid var(--ts-c-primary);
  border-bottom-right-radius: 100vw;
  pointer-events: none;
  transform: translateX(-100%);
  transition: width 475ms ease-out, transform 450ms ease, border-radius 0.8s 0.1s ease;
}

.main-navigation .menu-primary-container.show {
  transform: translateX(0);
  border-bottom-right-radius: 30px;
}

.main-navigation .menu-primary-container.show .menu-item {
  transform: translateX(0);
}

.main-navigation .menu-primary-container .menu {
  display: flex;
  flex-direction: column;
  height: 100%;
  list-style-type: none;
  pointer-events: auto;
}

.main-navigation .menu-primary-container .menu-item {
  margin-bottom: 20px;
  margin-left: 20px;
  font-size: 1.5rem;
  text-transform: capitalize;
  pointer-events: auto;
  transform: translateX(-100%);
}

.main-navigation .menu-primary-container .menu-item:nth-child(1) {
  transition: transform 1s 0.08s cubic-bezier(0.29, 1.4, 0.44, 0.96);
}

.main-navigation .menu-primary-container .menu-item:nth-child(2) {
  transition: transform 1s 0.16s cubic-bezier(0.29, 1.4, 0.44, 0.96);
}

.main-navigation .menu-primary-container .menu-item:nth-child(3) {
  transition: transform 1s 0.24s cubic-bezier(0.29, 1.4, 0.44, 0.96);
}

.main-navigation .menu-primary-container .menu-item:nth-child(4) {
  transition: transform 1s 0.32s cubic-bezier(0.29, 1.4, 0.44, 0.96);
}

.main-navigation .menu-primary-container .menu-item:nth-child(5) {
  transition: transform 1s 0.4s cubic-bezier(0.29, 1.4, 0.44, 0.96);
}

.main-navigation .menu-primary-container .menu-item:nth-child(6) {
  transition: transform 1s 0.48s cubic-bezier(0.29, 1.4, 0.44, 0.96);
}

.main-navigation .menu-primary-container .menu-item:nth-child(7) {
  transition: transform 1s 0.56s cubic-bezier(0.29, 1.4, 0.44, 0.96);
}

.main-navigation .menu-primary-container .menu-item:nth-child(8) {
  transition: transform 1s 0.64s cubic-bezier(0.29, 1.4, 0.44, 0.96);
}

.main-navigation .menu-primary-container .menu-item:nth-child(9) {
  transition: transform 1s 0.72s cubic-bezier(0.29, 1.4, 0.44, 0.96);
}

.main-navigation .menu-primary-container .menu-item:nth-child(10) {
  transition: transform 1s 0.8s cubic-bezier(0.29, 1.4, 0.44, 0.96);
}

.main-navigation .menu-primary-container .menu-item a {
  color: var(--ts-c-text-dark);
  pointer-events: auto;
  text-decoration: none;
}

@media only screen and (min-width: 48em) {
  .main-navigation .menu-primary-container {
    height: 80vh;
  }
}

@media only screen and (min-width: 64em) {
  .main-navigation {
    width: 100%;
    padding-left: 0;
  }

  .main-navigation .menu-primary-container {
    position: relative;
    top: 0;
    display: flex;
    height: 100%;
    width: 100%;
    justify-content: space-between;
    align-items: center;
    padding: 0;
    background-color: transparent;
    transform: none;
    transition: none;
  }

  .main-navigation .menu-primary-container.show {
    transform: none;
  }

  .main-navigation .menu-toggle {
    display: none !important;
  }

  .main-navigation .menu-primary-container .menu {
    flex-direction: row;
    margin: 0;
  }

  .main-navigation .menu-primary-container .menu-item {
    border: 3px solid transparent;
    margin: 0;
    padding: 5px;
    text-decoration: none;
    font-size: var(--ts-text-size);
    transform: none;
  }

  .main-navigation .menu-primary-container .menu-item:hover {
    border-bottom: 3px solid var(--ts-c-bg-light);
  }

  .main-navigation .menu-primary-container .menu-item a {
    color: var(--ts-c-text-light);
  }
}

@media only screen and (min-width: 64em) and (min-width: 80em) {
  .main-navigation .menu-primary-container .menu-item::before {
    margin: 0 30px;
  }
}
</style>
