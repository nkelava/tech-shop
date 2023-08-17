<script setup>
import { RouterLink } from "vue-router";
import UserIcon from "@/assets/icons/header/user.png";

const items = [
  { title: "My Account", to: "/user" },
  { title: "Admin", to: "/admin" },
  { title: "Logout", to: "/logout" },
];
</script>

<template>
  <div class="text-center">
    <v-menu transition="scale-transition">
      <template v-slot:activator="{ props }">
        <v-btn v-bind="props" class="widget__btn" variant="text">
          <img :src="UserIcon" alt="favorites icon" class="dropdown__icon" />
        </v-btn>
      </template>

      <v-list>
        <v-list-item v-for="(item, i) in items" :key="i">
          <router-link :to="item.to" class="link">{{ item.title }}</router-link>
        </v-list-item>
      </v-list>
    </v-menu>
  </div>
</template>

<style scoped>
.link {
  color: var(--ts-c-text-dark);
}
</style>
