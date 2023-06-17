<template>
  <v-card class="mx-auto">
    <v-list lines="10">
      <v-list-item>
        <template v-slot:subtitle>
          <span class="font-weight-bold">Ali Connors</span> &mdash; I'll be in your neighborhood
          doing errands this weekend. Do you want to hang out?
        </template>
      </v-list-item>

      <v-divider inset></v-divider>

      <v-list-item>
        <template v-slot:subtitle>
          <span class="font-weight-bold">Alex Scott</span> &mdash; Wish I could come, but I'm out of
          town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come,
          but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend.
          Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of
          town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come,
          but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend.
          Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of
          town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come,
          but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend.
          Wish I could come, but I'm out of town this weekend.
        </template>
      </v-list-item>

      <v-divider inset></v-divider>

      <v-list-item>
        <template v-slot:subtitle>
          <span class="font-weight-bold">Sandra Adams</span> &mdash; Do you have Paris
          recommendations? Have you ever been?
        </template>
      </v-list-item>
    </v-list>
  </v-card>
</template>

<style scoped>
.v-list {
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
}
.v-list-item {
  margin: 1rem auto;
}
</style>
