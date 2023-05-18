<script setup>
import { RouterView } from "vue-router";
import TheHeader from "@/components/TheHeader.vue";
import TheNewsletters from "@/components/TheNewsletters.vue";
import TheFooter from "@/components/TheFooter.vue";
</script>

<template>
  <div>
    <TheHeader />
    <RouterView />
    <TheNewsletters />
    <TheFooter />
  </div>
</template>
