<template>
  <div class="column is-one-third-desktop is-full-touch">
    <div class="card is-relative">
      <div class="overlay has-text-centered">
        <div class="information">
          <h3 class="title has-text-white is-size-2">Hello</h3>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { ref } from "vue";

const data = ref({
  filmingLocation: {
    city: "Los Angeles",
    state: "California",
  },
  vehicle: {
    make: "Honda",
    model: "S2000 2Fast 2Furious",
    originCountry: "Japan",
    year: "2004",
    id: 982,
  },
  dailyScore: {
    comfort: 4,
    features: 3,
    practicality: 1,
    quality: 4,
    value: 6,
    total: 18,
    id: 977,
  },
  weekendScore: {
    acceleration: 1,
    coolFactor: 8,
    funFactor: 6,
    handling: 7,
    styling: 6,
    total: 28,
    id: 977,
  },
  videoLink: "https://www.youtube.com/watch?v=4LAGNeE5pjA",
  totalDougScore: 46,
  id: 446,
});
</script>
<style lang="scss">
.card {
  min-height: 200px;
}

.overlay {
  background-color: black;
  opacity: 0.5;
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  margin-left: auto;
  margin-right: auto;
  width: 100%; /* Need a specific value to work */
  z-index: 2;
  height: 100%;

  .information {
    margin-top: 1rem;
    height: 100%;
    .title {
      text-overflow: ellipsis;
      overflow: hidden;
      white-space: nowrap;
      max-width: 95%;
      margin-left: auto;
      margin-right: auto;
    }
  }
}
</style>
