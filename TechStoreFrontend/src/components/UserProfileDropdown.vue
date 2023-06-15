<script setup>
import { RouterLink } from "vue-router";
import UserIcon from "@/assets/icons/header/user.png";

const items = [
  { title: "My Account", to: "/user" },
  { title: "Logout", to: "/logout" },
];
</script>

<template>
  <div class="text-center">
    <v-menu transition="scale-transition">
      <template v-slot:activator="{ props }">
        <v-btn v-bind="props" variant="text">
          <img :src="UserIcon" alt="favorites icon" class="dropdown__icon" />
        </v-btn>
      </template>

      <v-list>
        <v-list-item v-for="(item, i) in items" :key="i">
          <RouterLink :to="item.to">{{ item.title }}</RouterLink>
        </v-list-item>
      </v-list>
    </v-menu>
  </div>
</template>
